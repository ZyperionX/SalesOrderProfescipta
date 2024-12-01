import './App.css';

import { BrowserRouter, Route, Routes } from 'react-router-dom';
import SalesOrderList from './components/SalesOrderList';
import CreateSalesOrder from './components/CreateSalesOrder';
import { Navbar } from "react-bootstrap";


function App() {
    const navbar = () => {
        if (window.location.pathname === '/') {
            return (
                <Navbar.Brand className='text-white px-4 fw-bold'>Sales Order</Navbar.Brand>
            );
        }
    }
    return (
        <>
            <Navbar className="bg-dark">
                {navbar()}
            </Navbar>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={SalesOrderList()} />
                    <Route path="/create-sales-order" element={CreateSalesOrder()} />
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default App;