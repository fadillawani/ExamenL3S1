<?php
namespace App\Entity\Enum;
enum RoleUser: string {
    case GESTIONNAIRE = 'Gestionnaire';
    case CLIENT = 'CLIENT';
    case LIVREUR = 'LIVREUR';
}