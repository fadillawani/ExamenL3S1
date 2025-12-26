<?php

namespace App\Entity;

use App\Entity\Enum\TypeComplement;
use App\Repository\ComplementRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: ComplementRepository::class)]
class Complement
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(type:"string")]
    private ?string $libelle = null;

    #[ORM\Column(type:"float")]
    private ?float $prix = null;

    #[ORM\Column(name:"image_url", type:"string", nullable:true)]
    private ?string $imageUrl = null;

    #[ORM\Column(name:"is_archived", type:"boolean", nullable:true)]
    private ?bool $isArchived = null;

    #[ORM\Column(enumType: TypeComplement::class)]
    private ?TypeComplement $typecomplement = null;

    /**
     * @var Collection<int, MenuComplement>
     */
    #[ORM\OneToMany(mappedBy:"complement", targetEntity:MenuComplement::class)]
    private Collection $menuComplements;

    /**
     * @var Collection<int, PanierItem>
     */
    #[ORM\OneToMany(targetEntity: PanierItem::class, mappedBy: 'complement')]
    private Collection $panierItems;

    public function __construct()
    {
        $this->menuComplements = new ArrayCollection();
        $this->panierItems = new ArrayCollection();
    }

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getLibelle(): ?string
    {
        return $this->libelle;
    }

    public function setLibelle(string $libelle): static
    {
        $this->libelle = $libelle;

        return $this;
    }

    public function getPrix(): ?float
    {
        return $this->prix;
    }

    public function setPrix(float $prix): static
    {
        $this->prix = $prix;

        return $this;
    }

    public function getImageUrl(): ?string
    {
        return $this->imageUrl;
    }

    public function setImageUrl(string $imageUrl): static
    {
        $this->imageUrl = $imageUrl;

        return $this;
    }

    public function isArchived(): ?bool
    {
        return $this->isArchived;
    }

    public function setIsArchived(bool $isArchived): static
    {
        $this->isArchived = $isArchived;

        return $this;
    }

    public function getTypecomplement(): ?TypeComplement
    {
        return $this->typecomplement;
    }

    public function setTypecomplement(TypeComplement $typecomplement): static
    {
        $this->typecomplement = $typecomplement;

        return $this;
    }

    /**
     * @return Collection<int, MenuComplement>
     */
    public function getMenuComplements(): Collection
    {
        return $this->menuComplements;
    }

    public function addMenuComplement(MenuComplement $menuComplement): static
    {
        if (!$this->menuComplements->contains($menuComplement)) {
            $this->menuComplements->add($menuComplement);
            $menuComplement->setComplement($this);
        }

        return $this;
    }

    public function removeMenuComplement(MenuComplement $menuComplement): static
    {
        if ($this->menuComplements->removeElement($menuComplement)) {
            // set the owning side to null (unless already changed)
            if ($menuComplement->getComplement() === $this) {
                $menuComplement->setComplement(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, PanierItem>
     */
    public function getPanierItems(): Collection
    {
        return $this->panierItems;
    }

    public function addPanierItem(PanierItem $panierItem): static
    {
        if (!$this->panierItems->contains($panierItem)) {
            $this->panierItems->add($panierItem);
            $panierItem->setComplement($this);
        }

        return $this;
    }

    public function removePanierItem(PanierItem $panierItem): static
    {
        if ($this->panierItems->removeElement($panierItem)) {
            // set the owning side to null (unless already changed)
            if ($panierItem->getComplement() === $this) {
                $panierItem->setComplement(null);
            }
        }

        return $this;
    }
}
