<?php

namespace App\Entity;

use App\Repository\MenuComplementRepository;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: MenuComplementRepository::class)]
class MenuComplement
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\ManyToOne(targetEntity:Menu::class, inversedBy:"menuComplements")]
    #[ORM\JoinColumn(nullable:true)]
    private ?Menu $menu = null;

    #[ORM\ManyToOne(targetEntity:Complement::class, inversedBy:"menuComplements")]
    #[ORM\JoinColumn(nullable:true)]
    private ?Complement $complement = null;

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
}
