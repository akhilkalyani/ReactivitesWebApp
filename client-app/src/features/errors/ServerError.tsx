import { Container, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";

// eslint-disable-next-line react-refresh/only-export-components
export default observer(function ServerError(){
    const {commonStore}=useStore();
    return(
        <Container>
            <Header as='h1' content='Server Error' />
            <Header as='h5' color="red" content={commonStore.error?.message} />
            <Segment>
                <Header as='h4' content='Stack Trace' color="teal" />
                <code style={{marginTop:'10px'}}>{commonStore.error?.details}</code>
            </Segment>
        </Container>
    )
})