<?php

namespace App\Entity;

use App\Repository\BurgerRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\DBAL\Types\Types;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: "App\Repository\BurgerRepository")]
class Burger
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column(type: "bigint")]
    private ?int $id = null;

    #[ORM\Column(type: "string")]
    private ?string $libelle = null;

    #[ORM\Column(type: "text", nullable: true)]
    private ?string $description = null;

    #[ORM\Column]
    private ?float $prix = null;

    #[ORM\Column(name: "image_url", type: "string", nullable: true)]
    private ?string $imageUrl = null;

    #[ORM\Column(name: "is_archived", type: "boolean", nullable: true)]
    private ?bool $isArchived = null;

    #[ORM\ManyToOne(targetEntity: BurgerCategorie::class, inversedBy: "burgers")]
    #[ORM\JoinColumn(name: "burger_categorie_id", referencedColumnName: "id")]
    private ?BurgerCategorie $burgercategorie = null;

    /**
     * @var Collection<int, MenuBurger>
     */
    #[ORM\OneToMany(targetEntity: MenuBurger::class, mappedBy: 'burger')]
    private Collection $menuBurgers;

    /**
     * @var Collection<int, PanierItem>
     */
    #[ORM\OneToMany(targetEntity: PanierItem::class, mappedBy: 'burger')]
    private Collection $panierItems;

    public function __construct()
    {
        $this->menuBurgers = new ArrayCollection();
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

    public function getDescription(): ?string
    {
        return $this->description;
    }

    public function setDescription(string $description): static
    {
        $this->description = $description;

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

    public function getBurgercategorie(): ?BurgerCategorie
    {
        return $this->burgercategorie;
    }

    public function setBurgercategorie(?BurgerCategorie $burgercategorie): static
    {
        $this->burgercategorie = $burgercategorie;

        return $this;
    }

    /**
     * @return Collection<int, MenuBurger>
     */
    public function getMenuBurgers(): Collection
    {
        return $this->menuBurgers;
    }

    public function addMenuBurger(MenuBurger $menuBurger): static
    {
        if (!$this->menuBurgers->contains($menuBurger)) {
            $this->menuBurgers->add($menuBurger);
            $menuBurger->setBurger($this);
        }

        return $this;
    }

    public function removeMenuBurger(MenuBurger $menuBurger): static
    {
        if ($this->menuBurgers->removeElement($menuBurger)) {
            // set the owning side to null (unless already changed)
            if ($menuBurger->getBurger() === $this) {
                $menuBurger->setBurger(null);
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
            $panierItem->setBurger($this);
        }

        return $this;
    }

    public function removePanierItem(PanierItem $panierItem): static
    {
        if ($this->panierItems->removeElement($panierItem)) {
            // set the owning side to null (unless already changed)
            if ($panierItem->getBurger() === $this) {
                $panierItem->setBurger(null);
            }
        }

        return $this;
    }
}
