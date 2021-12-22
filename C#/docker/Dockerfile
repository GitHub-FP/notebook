FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 

RUN apt-get update 
RUN	apt-get install -y nginx

WORKDIR /usr/src/app/

COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY ./sites-enabled.conf /etc/nginx/sites-enabled/default
COPY ./service service
COPY ./dist /usr/etc/webroot
COPY ARIAL.TTF /usr/share/fonts/ARIAL.TTF


RUN chmod 777 /var/log/nginx 
RUN chmod 777 /etc/nginx/nginx.conf 
RUN chmod 777 /var/lib/nginx
RUN chmod 777 /etc/nginx/conf.d/default.conf



WORKDIR /usr/src/app/service

EXPOSE 8080
EXPOSE 8081

CMD nohup sh -c 'nginx && dotnet BIPortalService.dll --urls http://*:8081'
