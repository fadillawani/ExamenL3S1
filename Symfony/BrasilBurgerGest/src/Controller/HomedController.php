<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Attribute\Route;


class HomedController extends AbstractController
{
    #[Route('/', name: 'app_home')]
    public function index(): Response
    {
        // Redirection vers login si non connecté
        if (!$this->getUser()) {
            return $this->redirectToRoute('app_login');
        }

        // Sinon vers dashboard
        return $this->redirectToRoute('app_dashboard');
    }
}
