const https = require('https');
const fs = require('fs');
const path = require('path');
const Koa = require('koa');
const cors = require('@koa/cors');
const next = require('next');
const { default: createShopifyAuth } = require('@shopify/koa-shopify-auth');
const dotenv = require('dotenv');
const { verifyRequest } = require('@shopify/koa-shopify-auth');
const session = require('koa-session');
require('isomorphic-fetch');
dotenv.config();
var express = require('express')

//var cors = require('cors')
var myapp = express()
//const db = require('./db')
//db.collection('shopID').insert({ "name": "techAdmin" })
const request = require('request');
const port = parseInt(process.env.PORT, 10) || 3000;
const dev = process.env.NODE_ENV !== 'production';
const app = next({ dev });
const handle = app.getRequestHandler();
const { SHOPIFY_API_SECRET_KEY, SHOPIFY_API_KEY } = process.env;

app.prepare().then(() => {
  const server = new Koa();
  var optionss = {
    origin: '*'
  };
  server.use(cors(optionss));
  server.use(session(server));
  server.keys = [SHOPIFY_API_SECRET_KEY];
  server.use(
    createShopifyAuth({
      apiKey: SHOPIFY_API_KEY,
      secret: SHOPIFY_API_SECRET_KEY,
      scopes: ['read_products', 'read_themes', 'write_themes'],
      afterAuth(ctx) {
        const { shop, accessToken } = ctx.session;
        ctx.cookies.set('shopOrigin', shop, { httpOnly: false })
        //ctx.redirect('https://' + ctx.session.shop + '/admin/apps/customer-support-online-chat');
        ctx.redirect('/');
      },
    }),
  );
  //myapp.use(cors())
  server.use(verifyRequest());
  server.use(async (ctx) => {
    const { shop, accessToken } = ctx.session;
    const finalData = {
      shop,
      accessToken,
      SHOPIFY_API_SECRET_KEY,
      SHOPIFY_API_KEY
    }
    request.post('https://shopifyapp.botcreator.online:5000/storeMongo', { form: finalData }, (err, res, body) => {
      if (err) console.log('err', err);
    })
    await handle(ctx.req, ctx.res);
    ctx.respond = false;
    ctx.res.statusCode = 200;
    ctx.response.set('X-Frame-Options', `allow-from https://${shop}`);
    ctx.response.set('Content-Security-Policy', `frame-ancestors *.myshopify.com`);

  });
  const config = {
    https: {
      port: 443, // any port that is open and not already used on your server
      options: {
        key: fs.readFileSync(path.resolve(process.cwd(), '/etc/letsencrypt/live/shopifyapp.botcreator.online/privkey.pem'), 'utf8').toString(),
        cert: fs.readFileSync(path.resolve(process.cwd(), '/etc/letsencrypt/live/shopifyapp.botcreator.online/fullchain.pem'), 'utf8').toString(),
      },
    },
  };
  const serverCallback = server.callback();
  try {
    const httpsServer = https.createServer(config.https.options, serverCallback);
    httpsServer
      .listen(config.https.port, function (err) {
        if (!!err) {
          console.error('HTTPS server FAIL: ', err, (err && err.stack));
        }
        else {
          console.log(`HTTPS server OK: https://${config.domain}:${config.https.port}`);
        }
      });
  }
  catch (ex) {
    console.error('Failed to start HTTPS server\n', ex, (ex && ex.stack));
  }
});