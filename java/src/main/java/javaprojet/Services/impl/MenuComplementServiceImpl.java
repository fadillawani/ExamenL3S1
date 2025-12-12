package javaprojet.Services.impl;
import javaprojet.Entity.MenuComplement;
import javaprojet.Repository.MenuComplementRepository;
import javaprojet.Services.MenuComplementService;

import java.util.List;
import java.util.Optional;

public class MenuComplementServiceImpl implements MenuComplementService {

    private MenuComplementRepository menuComplementRepository;

    public MenuComplementServiceImpl(MenuComplementRepository menuComplementRepository) {
        this.menuComplementRepository = menuComplementRepository;
    }

    @Override
    public void createMenuComplement(MenuComplement menuComplement) {
        menuComplementRepository.insert(menuComplement);
    }

    @Override
    public Optional<MenuComplement> selectById(int id) {
        return menuComplementRepository.selectById(id);
    }

    @Override
    public List<MenuComplement> selectAll() {
        return menuComplementRepository.selectAll();
    }

    @Override
    public int numberOfRows()
    {
        return menuComplementRepository.numberOfRows();
    }

    @Override
    public List<MenuComplement> findByMenuId(int menuId) {
        return menuComplementRepository.findByMenuId(menuId);
    }


}
