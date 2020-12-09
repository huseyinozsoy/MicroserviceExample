const { request } = require("express");
const express = require("express");
const Product = require("../models/Product");
var ObjectID = require("mongodb").ObjectID;
const router = express.Router();

router.get("/catalog", async (req, res) => {
	const products = await Product.find();
	res.send(products);
});
router.get("/catalog/:id", async (req, res) => {
    const Id = req.params.id;
    const product = await Product.findById(Id);
    res.send(product);
});
router.get("/catalog/GetProductByCategory/:category", async (req, res) => {
    const category = req.params.category;
    const product = await Product.find({Category : category});
    res.send(product);
});
module.exports = router