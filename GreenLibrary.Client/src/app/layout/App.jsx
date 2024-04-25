import { Fragment } from "react";
import NavBar from "./NavBar";
import Home from "../../pages/Home";
import ArticleForm from "../../components/ArticleForm";
//import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
//import ArticleForm from "../../components/ArticleForm";

function App() {
    
    return (
        <Fragment>
            <NavBar />
            <Home />
            <ArticleForm />
        </Fragment>
    );
}

export default App;
