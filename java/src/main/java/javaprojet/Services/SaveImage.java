package javaprojet.Services;

import com.cloudinary.Cloudinary;
import com.cloudinary.utils.ObjectUtils;
import javax.swing.*;
import java.io.File;
import java.util.Map;

public class SaveImage {

    private Cloudinary cloudinary;

    public SaveImage() {
        cloudinary = new Cloudinary(ObjectUtils.asMap(
            "cloud_name", "dew0ggqu7",
            "api_key", "734913356445661",
            "api_secret", "hV7KtHsKdMvBEKwNZiMG7G4Y20U"
        ));
    }

    @SuppressWarnings("unchecked") 
    public String uploadImage() {
        System.out.println("Une fenêtre va s’ouvrir pour sélectionner une image...");

        JFileChooser chooser = new JFileChooser();
        chooser.setDialogTitle("Sélectionner une image");
        chooser.setFileSelectionMode(JFileChooser.FILES_ONLY);
        chooser.setAcceptAllFileFilterUsed(false);
        chooser.addChoosableFileFilter(
                new javax.swing.filechooser.FileNameExtensionFilter(
                        "Images", "jpg", "jpeg", "png", "webp"
                )
        );

        int result = chooser.showOpenDialog(null);

        if (result == JFileChooser.APPROVE_OPTION) {
            File selectedFile = chooser.getSelectedFile();
            System.out.println("Fichier sélectionné : " + selectedFile.getAbsolutePath());

            try {
                Map<String, Object> uploadResult = (Map<String, Object>) cloudinary.uploader().upload(
                        selectedFile,
                        ObjectUtils.emptyMap()
                );

                String url = uploadResult.get("secure_url").toString();
                System.out.println("Image uploadée avec succès !");
                System.out.println("URL Cloudinary : " + url);
                return url;

            } catch (Exception e) {
                e.printStackTrace();
                System.out.println("Erreur lors de l'upload : " + e.getMessage());
                return null;
            }

        } else {
            System.out.println("Sélection annulée.");
            return null;
        }
    }
}
