<?php

namespace App\Controller;

use App\Entity\Commande;
use App\Entity\Paiement;
use App\Entity\PanierItem;
use App\Entity\Enum\StatutCommande;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Attribute\Route;

final class DashboardController extends AbstractController
{
    #[Route('/dashboard', name: 'app_dashboard')]
    public function dashboard(EntityManagerInterface $em): Response
    {
        $start = new \DateTime('today 00:00:00');
        $end   = new \DateTime('today 23:59:59');

        /* ============================
           1. Commandes en cours du jour
        ============================ */
        $countEnCours = $em->createQueryBuilder()
            ->select('COUNT(c.id)')
            ->from(Commande::class, 'c')
            ->where('c.etat = :etat')
            ->andWhere('c.datecommande BETWEEN :start AND :end')
            ->setParameter('etat', StatutCommande::EN_ATTENTE)
            ->setParameter('start', $start)
            ->setParameter('end', $end)
            ->getQuery()
            ->getSingleScalarResult();

        /* ============================
           2. Commandes validées du jour
        ============================ */
        $countValidees = $em->createQueryBuilder()
            ->select('COUNT(c.id)')
            ->from(Commande::class, 'c')
            ->where('c.etat = :etat')
            ->andWhere('c.datecommande BETWEEN :start AND :end')
            ->setParameter('etat', StatutCommande::VALIDEE)
            ->setParameter('start', $start)
            ->setParameter('end', $end)
            ->getQuery()
            ->getSingleScalarResult();

        /* ============================
           3. Commandes annulées du jour
        ============================ */
        $countAnnulees = $em->createQueryBuilder()
            ->select('COUNT(c.id)')
            ->from(Commande::class, 'c')
            ->where('c.etat = :etat')
            ->andWhere('c.datecommande BETWEEN :start AND :end')
            ->setParameter('etat', StatutCommande::ANNULEE)
            ->setParameter('start', $start)
            ->setParameter('end', $end)
            ->getQuery()
            ->getSingleScalarResult();

        /* ============================
           4. Recettes journalières
           (commandes validées ou terminées)
        ============================ */
        $recettes = $em->createQueryBuilder()
            ->select('COALESCE(SUM(p.montant), 0)')
            ->from(Paiement::class, 'p')
            ->join('p.commande', 'c')
            ->where('c.etat IN (:etats)')
            ->andWhere('c.datecommande BETWEEN :start AND :end')
            ->setParameter('etats', [
                StatutCommande::VALIDEE,
                StatutCommande::TERMINEE,
            ])
            ->setParameter('start', $start)
            ->setParameter('end', $end)
            ->getQuery()
            ->getSingleScalarResult();

        /* ============================
           5. Burgers les plus vendus du jour
           (via PanierItem → Panier → Commande)
        ============================ */
        $topBurgers = $em->createQueryBuilder()
            ->select('b.libelle AS burger, SUM(pi.quantite) AS totalVendus')
            ->from(PanierItem::class, 'pi')
            ->join('pi.burger', 'b')
            ->join('pi.panier', 'p')
            ->join('p.commandes', 'c')
            ->where('c.etat IN (:etats)')
            ->andWhere('c.datecommande BETWEEN :start AND :end')
            ->groupBy('b.id')
            ->orderBy('totalVendus', 'DESC')
            ->setMaxResults(5)
            ->setParameter('etats', [
                StatutCommande::VALIDEE,
                StatutCommande::TERMINEE,
            ])
            ->setParameter('start', $start)
            ->setParameter('end', $end)
            ->getQuery()
            ->getResult();

        return $this->render('dashboard/index.html.twig', [
            'enCours'     => $countEnCours,
            'validees'    => $countValidees,
            'annulees'    => $countAnnulees,
            'recettes'    => $recettes,
            'topBurgers'  => $topBurgers,
        ]);
    }
}
