import NavBar from "./elements/NavBar";
import Home from "./pages/Home";
import ArticleForm from "./pages/CreateArticle";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


function App() {
    
    return (
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/create" element={<ArticleForm />} />
            </Routes>
        </Router>
    );
}

export default App;
