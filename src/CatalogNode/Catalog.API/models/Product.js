const mongoose = require("mongoose");

const schema = mongoose.Schema({
    //Id: mongoose.Schema.Types.ObjectId,
	Name: {type: String},
    Category: {type: String},
    Summary: {type: String},
    Description: {type: String},
    ImageFile: {type: String},
    Price: {type: Number}
})

module.exports = mongoose.model("Model", schema,"Products")