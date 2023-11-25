import { Button, Container, Menu } from "semantic-ui-react";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";


// eslint-disable-next-line react-refresh/only-export-components
export default observer(function Navbar(){
    const {activityStore}=useStore();
    return(
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="assets/logo.png" alt="logo" style={{marginRight:'10px'}}/>
                    Ractivities
                </Menu.Item>
                <Menu.Item name="Activities"/>
                <Menu.Item>
                    <Button onClick={()=>activityStore.openForm()} positive content='Create Activity'></Button>
                </Menu.Item>
            </Container>
        </Menu>
    )
})