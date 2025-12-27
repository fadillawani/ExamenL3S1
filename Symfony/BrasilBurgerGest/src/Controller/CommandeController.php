<?php

namespace App\Controller;

use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Attribute\Route;
use App\Entity\Commande;
use App\DTO\CommandeListDto;


final class CommandeController extends AbstractController
{
    #[Route('/commande', name: 'app_commande_list', methods: ['GET'])]
    public function list(EntityManagerInterface $em): Response
    {
        // 1️⃣ Récupération des commandes
        $commandes = $em->getRepository(Commande::class)->findAll();

        // 2️⃣ Transformation en DTO
        $commandeDtos = array_map(
            fn (Commande $commande) => CommandeListDto::fromEntity($commande),
            $commandes
        );

        // 3️⃣ Envoi à la vue
        return $this->render('commande/list.html.twig', [
            'commandes' => $commandeDtos
        ]);
    }
}
