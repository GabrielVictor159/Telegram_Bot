cd src/
docker-compose -f docker-compose.production.yml up -d
docker cp ./src/Telegram.BOT.WebMVC/wwwroot telegrambot-telegram.bot.webmvc-1:/app/wwwroot/
