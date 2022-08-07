import React from "react";
import {connect} from "react-redux";
import Header from "./Header";
import {logInActionCreator, logOutActionCreator} from "../../Redux/authReducer";


class HeaderContainer extends React.Component {
    render() {
        return (
            <Header {...this.props} />
        )
    }
}

let mapStateToProps = (state) => {
    return {
        isAuth: state.auth.isAuth,
        profileName: state.auth.profileName,
        profilePicture: state.auth.profilePicture
    }
};

export default connect(mapStateToProps, {logInActionCreator, logOutActionCreator})(HeaderContainer);
