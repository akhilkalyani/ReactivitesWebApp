import { Button, Container, Menu } from "semantic-ui-react";

interface Prop{
    openForm:()=>void;
}

export default function Navbar({openForm}:Prop){
    return(
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="assets/logo.png" alt="logo" style={{marginRight:'10px'}}/>
                    Ractivities
                </Menu.Item>
                <Menu.Item name="Activities"/>
                <Menu.Item>
                    <Button onClick={openForm} positive content='Create Activity'></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
}