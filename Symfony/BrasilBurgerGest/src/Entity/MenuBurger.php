<?php

namespace App\Entity;

use App\Repository\MenuBurgerRepository;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MenuBurgerRepository::class)]
class MenuBurger
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\ManyToOne(targetEntity:Menu::class, inversedBy:"menuBurgers")]
    #[ORM\JoinColumn(nullable:true)]
    private ?Menu $menu = null;

    #[ORM\ManyToOne(targetEntity:Burger::class, inversedBy:"menuBurgers")]
    #[ORM\JoinColumn(nullable:true)]
    private ?Burger $burger = null;

    #[ORM\Column(type:"integer")]
    private ?int $quantite = null;

    public function getId(): ?int
    {
        return $this->id;
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

    public function getBurger(): ?Burger
    {
        return $this->burger;
    }

    public function setBurger(?Burger $burger): static
    {
        $this->burger = $burger;

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
}
