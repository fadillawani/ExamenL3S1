<?php

namespace App\Controller;

use App\Entity\Commande;
use App\DTO\CommandeListDto;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Attribute\Route;
use App\Entity\Enum\StatutCommande;
use App\Form\CommandeFilterType;

final class CommandeController extends AbstractController
{
    #[Route('/commande', name: 'app_commande_list', methods: ['GET'])]
    public function list(Request $request, EntityManagerInterface $em): Response
    {
        // 1. Formulaire de filtres
        $form = $this->createForm(CommandeFilterType::class);
        $form->handleRequest($request);
        $filters = $form->getData();

        // 2. Pagination
        $page = max(1, $request->query->getInt('page', 1));
        $perPage = 10;
        $offset = ($page - 1) * $perPage;

        // 3. QueryBuilder dynamique
        $qb = $em->createQueryBuilder()
            ->select('c')
            ->from(Commande::class, 'c')
            ->leftJoin('c.client', 'u')
            ->orderBy('c.datecommande', 'DESC');

        if (!empty($filters['statut'])) {
            $qb->andWhere('c.etat = :statut')
               ->setParameter('statut', $filters['statut']);
        }

        if (!empty($filters['client'])) {
            $qb->andWhere('c.client = :client')
               ->setParameter('client', $filters['client']);
        }

        if (!empty($filters['dateDebut'])) {
            $qb->andWhere('c.datecommande >= :dateDebut')
               ->setParameter('dateDebut', $filters['dateDebut']->setTime(0, 0));
        }

        if (!empty($filters['dateFin'])) {
            $qb->andWhere('c.datecommande <= :dateFin')
               ->setParameter('dateFin', $filters['dateFin']->setTime(23, 59, 59));
        }

        // 4. Total pour pagination (on enlève orderBy pour PostgreSQL)
        $total = (clone $qb)
            ->select('COUNT(c.id)')
            ->resetDQLPart('orderBy')
            ->getQuery()
            ->getSingleScalarResult();

        // 5. Résultats paginés
        $commandesEntities = $qb
            ->setFirstResult($offset)
            ->setMaxResults($perPage)
            ->getQuery()
            ->getResult();

        // Transformation en DTO
        $commandes = array_map(fn($c) => CommandeListDto::fromEntity($c), $commandesEntities);

        $nbPages = ceil($total / $perPage);

        return $this->render('commande/list.html.twig', [
            'commandes' => $commandes,
            'filterForm' => $form->createView(),
            'currentPage' => $page,
            'nbPages' => $nbPages,
            'paginationRoute' => 'app_commande_list',
            'paginationParams' => $request->query->all(),
        ]);
    }

    #[Route('/commande/{id}', name: 'app_commande_detail', methods: ['GET'])]
    public function detail(int $id, EntityManagerInterface $em): Response
    {
        $commande = $em->getRepository(Commande::class)->find($id);
        if (!$commande) {
            throw $this->createNotFoundException('Commande non trouvée.');
        }

        $dto = \App\DTO\CommandeDetailDto::fromEntity($commande);

        return $this->render('commande/DetailsCommande.html.twig', [
            'commande' => $dto
        ]);
    }

    #[Route('/commande/{id}/valider', name: 'app_commande_valider', methods: ['GET'])]
    public function valider(int $id, EntityManagerInterface $em): Response
    {
        $commande = $em->getRepository(Commande::class)->find($id);
        if (!$commande || $commande->getEtat() !== StatutCommande::EN_ATTENTE) {
            throw $this->createNotFoundException();
        }

        $commande->setEtat(StatutCommande::VALIDEE);
        $em->flush();

        $this->addFlash('success', 'Commande validée avec succès.');
        return $this->redirectToRoute('app_commande_list');
    }

    #[Route('/commande/{id}/annuler', name: 'app_commande_annuler', methods: ['GET'])]
    public function annuler(int $id, EntityManagerInterface $em): Response
    {
        $commande = $em->getRepository(Commande::class)->find($id);
        if (!$commande || $commande->getEtat() !== StatutCommande::EN_ATTENTE) {
            throw $this->createNotFoundException();
        }

        $commande->setEtat(StatutCommande::ANNULEE);
        $em->flush();

        $this->addFlash('success', 'Commande annulée.');
        return $this->redirectToRoute('app_commande_list');
    }
}
