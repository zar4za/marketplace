const axios = require('axios');
//поменяй потом на импорт, я просто на ноде запускал этот файл

let descriptionHashmap = {};


//нам прилетает инвентарь в "неприятном формате", я решил сделать хештаблицу для описаний 
//чтобы связать два массива без перебора всех значений
function aggregateInventory(inventory) {
    createDescriptionsHashmap(inventory);
    
    let tradable = [];
    inventory.assets.forEach(item => {
        let description = descriptionHashmap[getHash(item)];

        if (description.tradable === 1) {
            tradable.push(description.market_hash_name);
        }
    });

    return tradable;
}

//тут мы как раз делаем эту таблицу, перебираем все описания и хешируем комбинацию classid и instanceid
function createDescriptionsHashmap(inventory) {
    inventory.descriptions.forEach(description => {
        descriptionHashmap[getHash(description)] = { 
            market_hash_name: description.market_hash_name,
            tradable: description.tradable
        };
    });
}

//хеширование до боли простое, просто складываем строки, если у предмета не существует instanceid то будет просто 0
function getHash (item) {
    return item.classid + '/' + item.instanceid;
}


// Это энтрипойнт для обработки инвентаря, чтобы я мог проверить на ноде
function fetchInventory (steamid) {
    let inventory;
    const uri = getInventoryUri(steamid);
    console.log(uri);
    axios.get(uri).then(x => inventory = x.data).then(x => console.log(aggregateInventory(x)));
}

//опять же проверки и тому подобное, это уже твоя задача писать на прекрасном языке JAVASCRIPT!
function getInventoryUri(steamid) {
    if (steamid.length !== 17) return null;
    return baseUri + steamid + suffixUri;
}

const baseUri = "https://steamcommunity.com/inventory/";
const suffixUri = "/730/2";

//УБЕРИ ЭТУ СТРОКУ ПОТОМ
fetchInventory('76561198106556563');