import NavBar from "./elements/NavBar";
import Home from "./pages/Home";
import CreateArticle from "./pages/CreateArticle";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ArticleDetails from "./pages/ArticleDetails";


function App() {
    
    return (
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/articles/:id" element={<ArticleDetails />}/>
                <Route path="/create" element={<CreateArticle />} />
            </Routes>
        </Router>
    );
}

export default App;
