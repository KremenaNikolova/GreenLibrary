import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import 'semantic-ui-css/semantic.min.css'
import './styles.css'
import { AuthProvider } from './hooks/AuthContext';

ReactDOM.createRoot(document.getElementById('root')).render(
    <AuthProvider>
        <App />
    </AuthProvider>
)
