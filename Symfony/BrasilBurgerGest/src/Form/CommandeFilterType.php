<?php

namespace App\Form;

use App\Entity\Users;
use App\Entity\Enum\StatutCommande;
use Symfony\Bridge\Doctrine\Form\Type\EntityType;
use Symfony\Component\Form\AbstractType;
use Symfony\Component\Form\Extension\Core\Type\ChoiceType;
use Symfony\Component\Form\Extension\Core\Type\DateType;
use Symfony\Component\Form\FormBuilderInterface;
use Symfony\Component\OptionsResolver\OptionsResolver;

class CommandeFilterType extends AbstractType
{
    public function buildForm(FormBuilderInterface $builder, array $options): void
    {
        $builder
            ->add('statut', ChoiceType::class, [
                'choices' => [
                    'En attente' => StatutCommande::EN_ATTENTE,
                    'Validée'    => StatutCommande::VALIDEE,
                    'Terminée'   => StatutCommande::TERMINEE,
                    'Annulée'    => StatutCommande::ANNULEE,
                ],
                'required' => false,
                'placeholder' => 'Tous les statuts',
            ])
            ->add('dateDebut', DateType::class, [
                'widget' => 'single_text',
                'required' => false,
                'label' => 'Date début',
            ])
            ->add('dateFin', DateType::class, [
                'widget' => 'single_text',
                'required' => false,
                'label' => 'Date fin',
            ])
            ->add('client', EntityType::class, [
                'class' => Users::class,
                'choice_label' => 'email',
                'required' => false,
                'placeholder' => 'Tous les clients',
            ]);
    }

    public function configureOptions(OptionsResolver $resolver): void
    {
        $resolver->setDefaults([
            'method' => 'GET', 
            'csrf_protection' => false,
        ]);
    }
}
