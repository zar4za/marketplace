import logo from './logo.svg';
import './App.css';
import Header from "./Components/Header/Header";
import {Route, Routes} from "react-router-dom";
import Inventory from "./Components/Inventory/Inventory";
import HeaderContainer from "./Components/Header/HeaderContainer";

function App() {
  return (
    <div className="app-wrapper">
        <HeaderContainer/>
        <div className="app-wrapper-content">
            <Routes>
                <Route path={'/inventory'} element={<Inventory />} />
            </Routes>
        </div>
    </div>
  );
}

export default App;
