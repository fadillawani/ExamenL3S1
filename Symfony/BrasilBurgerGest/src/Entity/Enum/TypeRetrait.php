<?php
namespace App\Entity\Enum;
enum TypeRetrait: string {
    case LIVRAISON = 'LIVRAISON';
    case SUR_PLACE = 'SUR_PLACE';
    case A_EMPORTER = 'A_EMPORTER';
}