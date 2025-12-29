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

final class CommandeController extends AbstractController
{
    #[Route('/commande', name: 'app_commande_list', methods: ['GET'])]
    public function list(Request $request, EntityManagerInterface $em): Response
    {
        
        $page = $request->query->getInt('page', 1); 
        $perPage = 10; 

        $allCommandes = $em->getRepository(Commande::class)
            ->findBy([], ['datecommande' => 'DESC']);

        $commandeDtos = array_map(
            fn(Commande $commande) => CommandeListDto::fromEntity($commande),
            $allCommandes
        );

        $totalCommandes = count($commandeDtos);
        $nbPages = ceil($totalCommandes / $perPage);
        $offset = ($page - 1) * $perPage;
        $commandesPage = array_slice($commandeDtos, $offset, $perPage);

        return $this->render('commande/list.html.twig', [
            'commandes' => $commandesPage,
            'currentPage' => $page,
            'nbPages' => $nbPages,
            'paginationRoute' => 'app_commande_list',
            'paginationParams' => [], 
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
