# =========================
# Image de base PHP CLI
# =========================
FROM php:8.2-cli

# =========================
# Variables d'environnement
# =========================
ENV APP_ENV=prod
ENV APP_DEBUG=0
ENV PORT=${PORT:-10000}

# =========================
# Dépendances système et extensions PHP
# =========================
RUN apt-get update && apt-get install -y \
    git \
    unzip \
    libpq-dev \
    libicu-dev \
    libzip-dev \
    zip \
    && docker-php-ext-install \
        pdo \
        pdo_pgsql \
        intl \
        zip \
        opcache

# =========================
# Installer Composer
# =========================
COPY --from=composer:2 /usr/bin/composer /usr/bin/composer

# =========================
# Définir le dossier de travail
# =========================
WORKDIR /app

# =========================
# Copier seulement Composer pour le cache Docker
# =========================
COPY composer.json composer.lock ./

# =========================
# Installer les dépendances sans dev ni scripts
# =========================
RUN composer install \
    --no-dev \
    --no-interaction \
    --prefer-dist \
    --optimize-autoloader \
    --no-scripts

# =========================
# Copier tout le projet
# =========================
COPY . .

# =========================
# Créer les dossiers nécessaires et définir les permissions
# =========================
RUN mkdir -p var/cache var/log var/sessions \
    && chmod -R 775 var \
    && chown -R www-data:www-data var

# =========================
# Cache Symfony en prod
# =========================
RUN php bin/console cache:clear --env=prod \
    && php bin/console cache:warmup

# =========================
# Exposer le port pour Render
# =========================
EXPOSE ${PORT}

# =========================
# Lancer le serveur PHP intégré Symfony
# =========================
CMD ["sh", "-c", "php -S 0.0.0.0:${PORT} -t public public/index.php"]
