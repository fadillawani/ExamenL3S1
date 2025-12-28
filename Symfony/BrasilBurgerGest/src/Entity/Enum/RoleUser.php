<?php
namespace App\Entity\Enum;
enum RoleUser: string {
    case GESTIONNAIRE = 'GESTIONNAIRE';
    case CLIENT = 'CLIENT';
    case LIVREUR = 'LIVREUR';
}