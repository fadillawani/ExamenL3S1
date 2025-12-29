<?php

namespace App\DTO;

use App\Entity\Commande;

class CommandeDetailDto
{
    public ?int $id = null;
    public ?\DateTimeInterface $dateCommande = null;
    public ?float $prixTotal = null;
    public ?string $etat = null;
    public ?string $clientNom = null;
    public ?string $clientEmail = null;
    public array $paiements = [];

    public static function fromEntity(Commande $commande): self
    {
        $dto = new self();
        $dto->id = $commande->getId();
        $dto->dateCommande = $commande->getDatecommande();
        $dto->prixTotal = $commande->getPrixtotal();
        $dto->etat = $commande->getEtat()?->name ?? null;

        if ($commande->getClient()) {
            $dto->clientNom = $commande->getClient()->getNom() ?? '';
            $dto->clientEmail = $commande->getClient()->getEmail() ?? '';
        }

    
        

        foreach ($commande->getPaiements() as $paiement) {
        $dto->paiements[] = [
        'id' => $paiement->getId(),
        'montant' => $paiement->getMontant(),
        'date' => $paiement->getDate(), // ajout de la date
    ];
}




        return $dto;
    }
}
