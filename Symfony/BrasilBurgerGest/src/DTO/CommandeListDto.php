<?php

namespace App\DTO;

use App\Entity\Commande;

class CommandeListDto
{
    public int $id;
    public string $dateCommande;
    public float $prixTotal;
    public string $etat;
    public int $clientId;
    public int $panierId;

    public ?string $emailClient=null;

    public array $paiements = [];

    public static function fromEntity(Commande $commande): self
    {
        $dto = new self();

        $dto->id = $commande->getId();
        $dto->dateCommande = $commande->getDatecommande()?->format('Y-m-d H:i');
        $dto->prixTotal = $commande->getPrixtotal();
        $dto->etat = $commande->getEtat()?->value;
        $dto->clientId = $commande->getClient()->getId();
        $dto->panierId = $commande->getPanier()->getId();
        $dto->paiements = $commande->getPaiements()->toArray();
        $dto->emailClient = $commande->getClient()->getEmail();
        return $dto;
    }
}
