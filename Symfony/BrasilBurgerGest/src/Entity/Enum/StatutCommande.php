<?php
namespace App\Entity\Enum;
enum StatutCommande: string {
    case EN_ATTENTE = 'EN_ATTENTE';
    case ANNULEE = 'ANNULEE';
    case TERMINEE = 'TERMINEE';
    case VALIDEE = 'VALIDEE';
}