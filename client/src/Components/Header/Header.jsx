import React from 'react';
import c from './Header.module.css';
import {NavLink} from "react-router-dom";

const Header = (props) => {

    return (
        <div className={c.headerWrapper}>
            <div className={c.logo}>
                <img src="https://pngimg.com/uploads/counter_strike/counter_strike_PNG100.png" alt=""/>
            </div>
            <div className={c.navBar}>
                <NavLink to={'/'} className={c.item}>Main</NavLink>
                <NavLink to={'/inventory'} className={c.item}>Inventory</NavLink>
            </div>
            <div className={c.loginBlock}>
                {props.isAuth ?
                    <div> <div> <img src={props.profilePicture} alt=""/></div> <div className={c.name}>{props.profileName} </div>
                        <div onClick={() => props.logOutActionCreator() } className={c.loginButton}>Log out</div></div> :
                <div onClick={() => props.logInActionCreator(1, "bigpatek", "https://avatars.akamai.steamstatic.com/272a40a1628e319ffd32946fe213884ae06ca84d_full.jpg")} className={c.loginButton}>Log in</div>}
            </div>
        </div>
    )
}


export default Header;
