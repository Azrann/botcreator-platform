
var mongoose = require('mongoose');
// var url = "REPLACE_WITH_YOUR_VALUE"
// var url ='mongodb://localhost:27017/shopifydb'
var url='mongodb://127.0.0.1:27017/shopifydb'
mongoose.connect(url, { useNewUrlParser: true });
var db = mongoose.connection
console.log('connection mongo done')
module.exports = db;
