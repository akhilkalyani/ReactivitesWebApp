import { Button, Container, Menu } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";


// eslint-disable-next-line react-refresh/only-export-components
export default observer(function Navbar(){
    return(
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src="assets/logo.png" alt="logo" style={{marginRight:'10px'}}/>
                    Ractivities
                </Menu.Item>
                <Menu.Item as={NavLink} to='/activities' name="Activities"/>
                <Menu.Item>
                    <Button as={NavLink} to='/createActivity' positive content='Create Activity'></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
})