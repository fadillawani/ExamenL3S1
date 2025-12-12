package javaprojet.Services.impl;
import javaprojet.Entity.MenuBurger;
import javaprojet.Repository.MenuBurgerRepository;
import javaprojet.Services.MenuBurgerService;

import java.util.List;
import java.util.Optional;

public class MenuBurgerServiceImpl implements MenuBurgerService {

    private MenuBurgerRepository menuBurgerRepository;

    public MenuBurgerServiceImpl(MenuBurgerRepository menuBurgerRepository) {
        this.menuBurgerRepository = menuBurgerRepository;
    }

    @Override
    public void createMenuBurger(MenuBurger menuBurger) {
        menuBurgerRepository.insert(menuBurger);
    }

    @Override
    public Optional<MenuBurger> selectById(int id) {
        return menuBurgerRepository.selectById(id);
    }

    @Override
    public List<MenuBurger> selectAll() {
        return menuBurgerRepository.selectAll();
    }

    @Override
    public int numberOfRows()
    {
        return menuBurgerRepository.numberOfRows();
    }

    @Override
    public List<MenuBurger> findByMenuId(int menuId) {
        return menuBurgerRepository.findByMenuId(menuId);
    }


}
