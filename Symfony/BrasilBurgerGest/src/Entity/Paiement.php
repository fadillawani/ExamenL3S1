<?php

namespace App\Entity;

use App\Entity\Enum\MoyenPaiement;
use App\Repository\PaiementRepository;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: PaiementRepository::class)]
class Paiement
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(type:"float")]
    private ?float $montant = null;

    #[ORM\Column(name:"ref_transaction", type:"string", nullable:true)]
    private ?string $refTransaction = null;

    #[ORM\Column(type:"datetime", nullable:true)]
    private ?\DateTime $date = null;

    #[ORM\Column(name: "moyen_paiement", enumType: MoyenPaiement::class, type: "string", length: 50)]
    private ?MoyenPaiement $moyenpaiement = null;

    #[ORM\ManyToOne(targetEntity:Commande::class)]
    #[ORM\JoinColumn(nullable:true, name:"commande_id", referencedColumnName:"id")]
    private ?Commande $commande = null;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getMontant(): ?float
    {
        return $this->montant;
    }

    public function setMontant(float $montant): static
    {
        $this->montant = $montant;

        return $this;
    }

    public function getRefTransaction(): ?string
    {
        return $this->refTransaction;
    }

    public function setRefTransaction(string $refTransaction): static
    {
        $this->refTransaction = $refTransaction;

        return $this;
    }

    public function getDate(): ?\DateTime
    {
        return $this->date;
    }

    public function setDate(\DateTime $date): static
    {
        $this->date = $date;

        return $this;
    }

    public function getMoyenpaiement(): ?MoyenPaiement
    {
        return $this->moyenpaiement;
    }

    public function setMoyenpaiement(MoyenPaiement $moyenpaiement): static
    {
        $this->moyenpaiement = $moyenpaiement;

        return $this;
    }

    public function getCommande(): ?Commande
    {
        return $this->commande;
    }

    public function setCommande(?Commande $commande): static
    {
        $this->commande = $commande;

        return $this;
    }
}
