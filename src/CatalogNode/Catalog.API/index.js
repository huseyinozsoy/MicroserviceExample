const express = require("express");
const mongoose = require("mongoose");
const routes = require("./routes/routes");
const app = express();
mongoose.connect("mongodb://localhost:27017/CatalogDb", { useNewUrlParser: true }, (err)=>{
    if(err)
        console.log(err);
    else
        console.log("Succesfully conected to DB");
});
app.use("/api/v1", routes);
app.listen(5000, () => {
    console.log("Server has started!")
}); 
