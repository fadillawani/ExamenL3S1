<?php

namespace App\Entity;

use App\Entity\Enum\StatutCommande;
use App\Repository\CommandeRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: CommandeRepository::class)]
class Commande
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(name:"date_commande", type:"datetime")]
    private ?\DateTime $datecommande = null;

    #[ORM\Column(name:"prix_total", type:"float")]
    private ?float $prixtotal = null;

    #[ORM\Column(enumType: StatutCommande::class)]
    private ?StatutCommande $etat = null;

    #[ORM\ManyToOne(targetEntity:Panier::class, inversedBy:"commandes")]
    #[ORM\JoinColumn(nullable:false, name:"panier_id", referencedColumnName:"id")]
    private ?Panier $panier = null;

    #[ORM\ManyToOne(targetEntity: Users::class, inversedBy: "commandes")]
    #[ORM\JoinColumn(name: "user_id", nullable: false)]
    private ?Users $client = null;

    /**
     * @var Collection<int, Paiement>
     */
    #[ORM\ManyToOne(targetEntity:Paiement::class)]
    #[ORM\JoinColumn(nullable:true, name:"paiement_id", referencedColumnName:"id")]
    private Collection $paiements;

    /**
     * @var Collection<int, LivraisonAffectation>
     */
    #[ORM\ManyToOne(targetEntity:LivraisonAffectation::class)]
    #[ORM\JoinColumn(nullable:true, name:"livraison_affectation_id", referencedColumnName:"id")]
    private Collection $livraisonAffectations;

    public function __construct()
    {
        $this->paiements = new ArrayCollection();
        $this->livraisonAffectations = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getDatecommande(): ?\DateTime
    {
        return $this->datecommande;
    }

    public function setDatecommande(\DateTime $datecommande): static
    {
        $this->datecommande = $datecommande;

        return $this;
    }

    public function getPrixtotal(): ?float
    {
        return $this->prixtotal;
    }

    public function setPrixtotal(float $prixtotal): static
    {
        $this->prixtotal = $prixtotal;

        return $this;
    }

    public function getEtat(): ?StatutCommande
    {
        return $this->etat;
    }

    public function setEtat(StatutCommande $etat): static
    {
        $this->etat = $etat;

        return $this;
    }

    public function getPanier(): ?Panier
    {
        return $this->panier;
    }

    public function setPanier(?Panier $panier): static
    {
        $this->panier = $panier;

        return $this;
    }

    public function getClient(): ?Users
    {
        return $this->client;
    }

    public function setClient(?Users $client): static
    {
        $this->client = $client;

        return $this;
    }

    /**
     * @return Collection<int, Paiement>
     */
    public function getPaiements(): Collection
    {
        return $this->paiements;
    }

    public function addPaiement(Paiement $paiement): static
    {
        if (!$this->paiements->contains($paiement)) {
            $this->paiements->add($paiement);
            $paiement->setCommande($this);
        }

        return $this;
    }

    public function removePaiement(Paiement $paiement): static
    {
        if ($this->paiements->removeElement($paiement)) {
            // set the owning side to null (unless already changed)
            if ($paiement->getCommande() === $this) {
                $paiement->setCommande(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, LivraisonAffectation>
     */
    public function getLivraisonAffectations(): Collection
    {
        return $this->livraisonAffectations;
    }

    public function addLivraisonAffectation(LivraisonAffectation $livraisonAffectation): static
    {
        if (!$this->livraisonAffectations->contains($livraisonAffectation)) {
            $this->livraisonAffectations->add($livraisonAffectation);
            $livraisonAffectation->setCommande($this);
        }

        return $this;
    }

    public function removeLivraisonAffectation(LivraisonAffectation $livraisonAffectation): static
    {
        if ($this->livraisonAffectations->removeElement($livraisonAffectation)) {
            // set the owning side to null (unless already changed)
            if ($livraisonAffectation->getCommande() === $this) {
                $livraisonAffectation->setCommande(null);
            }
        }

        return $this;
    }
}
