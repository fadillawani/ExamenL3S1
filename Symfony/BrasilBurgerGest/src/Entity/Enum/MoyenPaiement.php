<?php

namespace App\Entity\Enum;

enum MoyenPaiement: string {
    case WAVE = 'WAVE';
    case OM = 'OM';
}