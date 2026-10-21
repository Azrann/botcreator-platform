const fs = require('fs')
const https = require('https')
var express = require('express')
var app = express()
var cors = require('cors')
var bodyParser = require("body-parser");
app.use(cors())
var shopifyAPI = require('shopify-node-api');
app.use(bodyParser.urlencoded({ extended: false }));
var myBody = bodyParser.json();
const db = require('./db/db')
var ObjectId = require('mongodb').ObjectID;

db.collection("shopifydb").findOne({}, function (err, result) {  
  if (err) throw err;
  app.post('/storeMongo', myBody, (req, res) => {
    res.status(200).send({
      message: "success",
      body: req.finalData,
    })
    const shop_s = req.body.shop
    const accessToken_S = req.body.accessToken
    const SHOPIFY_API_KEY_S = req.body.SHOPIFY_API_KEY
    const SHOPIFY_API_SECRET_KEY_S = req.body.SHOPIFY_API_SECRET_KEY

    app.post('/api', myBody, (req, res) => {
      var formData = req.body.finalDAta
      console.log(formData)
      if (result == null) {
        db.collection('shopifydb').insert({
          formData, shop_s,
          accessToken_S,
          SHOPIFY_API_KEY_S,
          SHOPIFY_API_SECRET_KEY_S
        })
      } else if (result !== null) {
        db.collection('shopifydb').save({
          _id: ObjectId(result._id),
          formData, shop_s,
          accessToken_S,
          SHOPIFY_API_KEY_S,
          SHOPIFY_API_SECRET_KEY_S
        })
      }
      db.collection("shopifydb").findOne({}, { "shop_s": shop_s},function (err, mongoData) {
        if (err) throw err;     
        console.log(mongoData);  
        res.status(200).send({
          message: "success",
          body: mongoData,
        })
      });
      var Shopify = new shopifyAPI({
        shop: shop_s, // MYSHOP.myshopify.com
        shopify_api_key: SHOPIFY_API_KEY_S, // Your API key
        access_token: accessToken_S // Your API password
      });
      var shopifyId = []
      Shopify.get('/admin/themes.json?role=main', function (err, data, headers) {
        data.themes.map((f) => {
          if (f.id) {
            shopifyId.push(f.id)
          }
          console.log('---------------------',req.body.string)
          var put_data = {
            "asset": {
              "key": "snippets/botcreator.liquid",
              "value": req.body.string
            }
          }
          var myurl = `/admin/themes/${shopifyId}/assets.json`
          Shopify.put(myurl, put_data, function (err, data, headers) {

          });
        })
      })
    })
    app.get('/api/get', myBody, (req, res) => {
      var Shopify = new shopifyAPI({
        shop: shop_s, // MYSHOP.myshopify.com
        shopify_api_key: SHOPIFY_API_KEY_S, // Your API key
        access_token: accessToken_S // Your API password
      });
      var shopifyId = []
      Shopify.get('/admin/themes.json?role=main', function (err, data, headers) {
        data.themes.map((f) => {
          if (f.id) {
            shopifyId.push(f.id)
          }
          console.log(shopifyId,'------>>>');
          var myurl = `/admin/themes/${shopifyId}/assets.json?asset[key]=layout/theme.liquid`
          Shopify.get(myurl, function (err, data, headers) {
            // console.log('data------------------',data)
            res.status(200).send({
              data: data
            })
          });
        })
      })
    })

    app.post('/api/post', myBody, (req, res) => {

      var newValue1 = req.body.valueAsset.split('</body>')[0]
      var newValue2 = req.body.valueAsset.split('</body>')[1]
      var newValue3 = newValue1 + "{% include 'botcreator' %}</body>" + newValue2
      console.log('sst=e============',newValue3)
      var n4 = Buffer.from(req.body.valueAsset)
      var ext = n4.indexOf("{% include 'botcreator' %}")
       if (ext < 0) {
        var Shopify = new shopifyAPI({
          shop: shop_s, // MYSHOP.myshopify.com
          shopify_api_key: SHOPIFY_API_KEY_S, // Your API key
          access_token: accessToken_S // Your API password
        });
        var shopifyId = []
        Shopify.get('/admin/themes.json?role=main', function (err, data, headers) {
          data.themes.map((f) => {
            if (f.id) {
              shopifyId.push(f.id)
            }
            var put_data = {
              "asset": {
                "key": "layout/theme.liquid",
                "value": newValue3
              }
            }
            var myurl = `/admin/themes/${shopifyId}/assets.json`
            Shopify.put(myurl, put_data, function (err, data, headers) {
              res.status(200).send({
                data: data
              })
            });
          });
        })
       }
    })
  })
})

// app.get('/', (req, res) => {
//   res.send('Hello HTTPS!')
// })

https.createServer({
  key: fs.readFileSync('/etc/letsencrypt/live/shopifyapp.botcreator.online/privkey.pem'),
  cert: fs.readFileSync('/etc/letsencrypt/live/shopifyapp.botcreator.online/cert.pem'),
  ca: fs.readFileSync('/etc/letsencrypt/live/shopifyapp.botcreator.online/chain.pem')
}, app).listen(5000, () => {
  console.log('shopifyapp.botcreator.online:5000 Listening...')
})
