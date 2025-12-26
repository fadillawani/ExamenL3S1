<?php

namespace App\Entity;

use App\Repository\PanierItemRepository;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: PanierItemRepository::class)]
class PanierItem
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

     #[ORM\ManyToOne( targetEntity:Panier::class, inversedBy:"panierItems")]
    #[ORM\JoinColumn(nullable:false)]
    private ?Panier $panier = null;

    #[ORM\ManyToOne( targetEntity:Burger::class, inversedBy: "panierItems")]
    #[ORM\JoinColumn(name: "burger_id", referencedColumnName: "id")]
    private ?Burger $burger = null;

    #[ORM\ManyToOne( targetEntity:Menu::class, inversedBy: "panierItems")]
    #[ORM\JoinColumn(name: "menu_id", referencedColumnName: "id")]
    private ?Menu $menu = null;

    #[ORM\ManyToOne( targetEntity:Complement::class, inversedBy: "panierItems")]
    #[ORM\JoinColumn(name: "complement_id", referencedColumnName: "id")]
    private ?Complement $complement = null;

    #[ORM\Column(type:"integer")]
    private ?int $quantite = null;

    #[ORM\Column(name:"prix_total", type:"float")]
    private ?float $prixtotal = null;

    public function getId(): ?int
    {
        return $this->id;
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

    public function getBurger(): ?Burger
    {
        return $this->burger;
    }

    public function setBurger(?Burger $burger): static
    {
        $this->burger = $burger;

        return $this;
    }

    public function getMenu(): ?Menu
    {
        return $this->menu;
    }

    public function setMenu(?Menu $menu): static
    {
        $this->menu = $menu;

        return $this;
    }

    public function getComplement(): ?Complement
    {
        return $this->complement;
    }

    public function setComplement(?Complement $complement): static
    {
        $this->complement = $complement;

        return $this;
    }

    public function getQuantite(): ?int
    {
        return $this->quantite;
    }

    public function setQuantite(int $quantite): static
    {
        $this->quantite = $quantite;

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
}
