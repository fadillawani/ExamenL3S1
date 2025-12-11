package javaprojet.Services.impl;

import java.util.List;
import java.util.Optional;
import javaprojet.Entity.BurgerCategorie;
import javaprojet.Repository.BurgerCategorieRepository;
import javaprojet.Services.BurgerCategorieService;

public class BurgerCategorieServiceImpl implements BurgerCategorieService {

    private BurgerCategorieRepository burgerCategorieRepository;

    public BurgerCategorieServiceImpl(BurgerCategorieRepository burgerCategorieRepository) {
        this.burgerCategorieRepository = burgerCategorieRepository;
    }

    @Override
    public void createBurgerCategorie(BurgerCategorie burgerCategorie) {
        burgerCategorieRepository.insert(burgerCategorie);
    }

    @Override
    public Optional<BurgerCategorie> selectById(int id) {
        return burgerCategorieRepository.selectById(id);
    }

    @Override
    public List<BurgerCategorie> selectAll() {
        return burgerCategorieRepository.selectAll();
    }

    @Override
    public int numberOfRows()
    {
        return burgerCategorieRepository.numberOfRows();
    }

}
