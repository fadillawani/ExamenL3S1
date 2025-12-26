<?php

namespace App\Entity;

use App\Entity\Enum\RoleUser;
use App\Repository\UsersRepository;
use Doctrine\Common\Collections\ArrayCollection;
use Doctrine\Common\Collections\Collection;
use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity(repositoryClass: UsersRepository::class)]
class Users
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type:"bigint")]
    private ?int $id = null;

    #[ORM\Column(type:"string", length: 150)]
    private ?string $nom = null;

    #[ORM\Column(type:"string", length: 150)]
    private ?string $prenon = null;

    #[ORM\Column(type:"string", length: 50)]
    private ?string $tel = null;

    #[ORM\Column(name:"created_at", type:"datetime", nullable:true)]
    private ?\DateTimeImmutable $createdAt = null;

    #[ORM\Column(enumType: RoleUser::class)]
    private ?RoleUser $role = null;

    #[ORM\Column]
    private ?bool $isArchived = null;

    /**
     * @var Collection<int, Panier>
     */
    #[ORM\OneToMany( mappedBy: "client", targetEntity: Panier::class)]
    private Collection $paniers;

    /**
     * @var Collection<int, Commande>
     */
    #[ORM\OneToMany(mappedBy: "client", targetEntity: Commande::class)]
    private Collection $commandes;

    /**
     * @var Collection<int, LivraisonAffectation>
     */
    #[ORM\OneToMany(mappedBy: "livreur", targetEntity: LivraisonAffectation::class)]
    private Collection $livraisonAffectations;

    #[ORM\Column(type:"string", length: 255)]
    private ?string $email = null;

    #[ORM\Column(type:"string", length: 50)]
    private ?string $password = null;

    public function __construct()
    {
        $this->paniers = new ArrayCollection();
        $this->commandes = new ArrayCollection();
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

    public function getPrenon(): ?string
    {
        return $this->prenon;
    }

    public function setPrenon(string $prenon): static
    {
        $this->prenon = $prenon;

        return $this;
    }

    public function getTel(): ?string
    {
        return $this->tel;
    }

    public function setTel(string $tel): static
    {
        $this->tel = $tel;

        return $this;
    }

    public function getCreatedAt(): ?\DateTimeImmutable
    {
        return $this->createdAt;
    }

    public function setCreatedAt(\DateTimeImmutable $createdAt): static
    {
        $this->createdAt = $createdAt;

        return $this;
    }

    public function getRole(): ?RoleUser
    {
        return $this->role;
    }

    public function setRole(RoleUser $role): static
    {
        $this->role = $role;

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

    /**
     * @return Collection<int, Panier>
     */
    public function getPaniers(): Collection
    {
        return $this->paniers;
    }

    public function addPanier(Panier $panier): static
    {
        if (!$this->paniers->contains($panier)) {
            $this->paniers->add($panier);
            $panier->setClient($this);
        }

        return $this;
    }

    public function removePanier(Panier $panier): static
    {
        if ($this->paniers->removeElement($panier)) {
            // set the owning side to null (unless already changed)
            if ($panier->getClient() === $this) {
                $panier->setClient(null);
            }
        }

        return $this;
    }

    /**
     * @return Collection<int, Commande>
     */
    public function getCommandes(): Collection
    {
        return $this->commandes;
    }

    public function addCommande(Commande $commande): static
    {
        if (!$this->commandes->contains($commande)) {
            $this->commandes->add($commande);
            $commande->setClient($this);
        }

        return $this;
    }

    public function removeCommande(Commande $commande): static
    {
        if ($this->commandes->removeElement($commande)) {
            // set the owning side to null (unless already changed)
            if ($commande->getClient() === $this) {
                $commande->setClient(null);
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
            $livraisonAffectation->setLivreur($this);
        }

        return $this;
    }

    public function removeLivraisonAffectation(LivraisonAffectation $livraisonAffectation): static
    {
        if ($this->livraisonAffectations->removeElement($livraisonAffectation)) {
            // set the owning side to null (unless already changed)
            if ($livraisonAffectation->getLivreur() === $this) {
                $livraisonAffectation->setLivreur(null);
            }
        }

        return $this;
    }

    public function getEmail(): ?string
    {
        return $this->email;
    }

    public function setEmail(string $email): static
    {
        $this->email = $email;

        return $this;
    }

    public function getPassword(): ?string
    {
        return $this->password;
    }

    public function setPassword(string $password): static
    {
        $this->password = $password;

        return $this;
    }
}
