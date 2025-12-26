<?php

namespace App\Entity;

use App\Repository\ZoneRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: ZoneRepository::class)]
class Zone
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(type:"string", length: 255)]
    private ?string $nom = null;

    #[ORM\Column(name:"prix_livraison", type:"string", length: 255)]
    private ?string $prix_livraison = null;

    /**
     * @var Collection<int, Quartier>
     */
    #[ORM\OneToMany( mappedBy:"zone", targetEntity:Quartier::class)]
    private Collection $quartiers;

    /**
     * @var Collection<int, LivraisonAffectation>
     */
    #[ORM\OneToMany(targetEntity: LivraisonAffectation::class, mappedBy: 'zone')]
    private Collection $livraisonAffectations;

    public function __construct()
    {
        $this->quartiers = new ArrayCollection();
        $this->livraisonAffectations = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getNom(): ?string
    {
        return $this->nom;
    }

    public function setNom(string $nom): static
    {
        $this->nom = $nom;

        return $this;
    }

    public function getPrixLivraison(): ?string
    {
        return $this->prix_livraison;
    }

    public function setPrixLivraison(string $prix_livraison): static
    {
        $this->prix_livraison = $prix_livraison;

        return $this;
    }

    /**
     * @return Collection<int, Quartier>
     */
    public function getQuartiers(): Collection
    {
        return $this->quartiers;
    }

    public function addQuartier(Quartier $quartier): static
    {
        if (!$this->quartiers->contains($quartier)) {
            $this->quartiers->add($quartier);
            $quartier->setZone($this);
        }

        return $this;
    }

    public function removeQuartier(Quartier $quartier): static
    {
        if ($this->quartiers->removeElement($quartier)) {
            // set the owning side to null (unless already changed)
            if ($quartier->getZone() === $this) {
                $quartier->setZone(null);
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
            $livraisonAffectation->setZone($this);
        }

        return $this;
    }

    public function removeLivraisonAffectation(LivraisonAffectation $livraisonAffectation): static
    {
        if ($this->livraisonAffectations->removeElement($livraisonAffectation)) {
            // set the owning side to null (unless already changed)
            if ($livraisonAffectation->getZone() === $this) {
                $livraisonAffectation->setZone(null);
            }
        }

        return $this;
    }
}
