<?php

namespace App\Entity;

use App\Repository\MenuRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MenuRepository::class)]
class Menu
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(type:"string")]
    private ?string $libelle = null;

    #[ORM\Column(name:"image_url", type:"string", nullable:false)]
    private ?string $imageUrl = null;

    #[ORM\Column(name:"is_archived", type:"boolean", nullable:true)]
    private ?bool $isArchived = null;

    #[ORM\Column(type:"float")]
    private ?float $prix = null;

    /**
     * @var Collection<int, MenuBurger>
     */
    #[ORM\OneToMany(mappedBy:"menu", targetEntity:MenuBurger::class)]
    private Collection $menuBurgers;

    /**
     * @var Collection<int, MenuComplement>
     */
    #[ORM\OneToMany(mappedBy:"menu", targetEntity:MenuComplement::class)]
    private Collection $menuComplements;

    /**
     * @var Collection<int, PanierItem>
     */
    #[ORM\OneToMany(mappedBy:"menu", targetEntity:PanierItem::class)]
    private Collection $panierItems;

    public function __construct()
    {
        $this->menuBurgers = new ArrayCollection();
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

    public function getPrix(): ?float
    {
        return $this->prix;
    }

    public function setPrix(float $prix): static
    {
        $this->prix = $prix;

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
            $menuBurger->setMenu($this);
        }

        return $this;
    }

    public function removeMenuBurger(MenuBurger $menuBurger): static
    {
        if ($this->menuBurgers->removeElement($menuBurger)) {
            // set the owning side to null (unless already changed)
            if ($menuBurger->getMenu() === $this) {
                $menuBurger->setMenu(null);
            }
        }

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
            $menuComplement->setMenu($this);
        }

        return $this;
    }

    public function removeMenuComplement(MenuComplement $menuComplement): static
    {
        if ($this->menuComplements->removeElement($menuComplement)) {
            // set the owning side to null (unless already changed)
            if ($menuComplement->getMenu() === $this) {
                $menuComplement->setMenu(null);
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
            $panierItem->setMenu($this);
        }

        return $this;
    }

    public function removePanierItem(PanierItem $panierItem): static
    {
        if ($this->panierItems->removeElement($panierItem)) {
            // set the owning side to null (unless already changed)
            if ($panierItem->getMenu() === $this) {
                $panierItem->setMenu(null);
            }
        }

        return $this;
    }
}
