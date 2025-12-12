package javaprojet.Services.impl;
import javaprojet.Entity.Menu;
import javaprojet.Repository.MenuRepository;
import javaprojet.Services.MenuService;

import java.util.List;
import java.util.Optional;

public class MenuServiceImpl implements MenuService {

    private MenuRepository menuRepository;

    public MenuServiceImpl(MenuRepository menuRepository) {
        this.menuRepository = menuRepository;
    }

    @Override
    public void createMenu(Menu menu) {
        menuRepository.insert(menu);
    }


    @Override
    public int numberOfRows()
    {
        return menuRepository.numberOfRows();
    }

    @Override
    public void update(Menu menu) {
        menuRepository.update(menu);
    }

    @Override
    public Optional<Menu> selectById(int id) {
        return menuRepository.selectById(id);
    }

    @Override
    public List<Menu> selectAll() {
        return menuRepository.selectAll();
    }

}
