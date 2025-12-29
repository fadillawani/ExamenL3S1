<?php

namespace App\Entity;

use App\Entity\Enum\StatutLivraison;
use App\Repository\LivraisonAffectationRepository;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: LivraisonAffectationRepository::class)]
class LivraisonAffectation
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(enumType: StatutLivraison::class)]
    private ?StatutLivraison $statut = null;

    #[ORM\ManyToOne(targetEntity: Commande::class, inversedBy: "livraisonAffectations")]
    #[ORM\JoinColumn(nullable:true, name:"commande_id", referencedColumnName:"id")]
    private ?Commande $commande = null;

    #[ORM\ManyToOne(targetEntity: Users::class, inversedBy: "livraisonAffectations")]
    #[ORM\JoinColumn(name: "livreur_id", nullable: false)]
    private ?Users $livreur = null;

    #[ORM\ManyToOne(targetEntity: Zone::class, inversedBy: "livraisonAffectations")]
    #[ORM\JoinColumn(name: "zone_id", nullable: true)]
    private ?Zone $zone = null;

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getStatut(): ?StatutLivraison
    {
        return $this->statut;
    }

    public function setStatut(StatutLivraison $statut): static
    {
        $this->statut = $statut;

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

    public function getLivreur(): ?Users
    {
        return $this->livreur;
    }

    public function setLivreur(?Users $livreur): static
    {
        $this->livreur = $livreur;

        return $this;
    }

    public function getZone(): ?Zone
    {
        return $this->zone;
    }

    public function setZone(?Zone $zone): static
    {
        $this->zone = $zone;

        return $this;
    }
}
