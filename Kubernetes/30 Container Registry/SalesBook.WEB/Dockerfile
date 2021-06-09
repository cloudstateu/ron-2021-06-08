FROM php:7.3.6-apache-stretch

COPY . /srv/app
COPY vhost.conf /etc/apache2/sites-available/000-default.conf

RUN chown -R www-data:www-data /srv/app && a2enmod rewrite
