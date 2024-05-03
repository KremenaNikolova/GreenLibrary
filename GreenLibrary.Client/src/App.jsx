import NavBar from "./components/NavBar";
import Home from "./pages/Home";
import CreateArticle from "./pages/CreateArticle";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ArticleDetails from "./pages/ArticleDetails";
import CategoryArticlesList from "./pages/CategoryArticlesList";
import LoginForm from "./pages/LoginForm";
import RegisterForm from "./pages/RegisterForm";


function App() {
    
    return (
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/articles/:id" element={<ArticleDetails />}/>
                <Route path="/create" element={<CreateArticle />} />
                <Route path="/categories/:category" element={<CategoryArticlesList />} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/register" element={<RegisterForm />} />
            </Routes>
        </Router>
    );
}

export default App;
