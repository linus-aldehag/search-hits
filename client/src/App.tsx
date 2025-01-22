import { useState } from "react"
import axios from "axios"
import { Button, Card, Center, Flex, Input, List, Spinner } from "@chakra-ui/react"

export default function App() {
    interface Result {
        input: string,
        hits: number
    }
    
    const [input, setInput] = useState("")
    const [result, setResult] = useState<Result[]>([])
    const [loading, setLoading] = useState(false)

    const sendRequest = () => {
        setLoading(true);
        setResult([]);
        
        axios.get("/api/" + input)
        .then(res => setResult(res.data))
        .catch(err => console.log(err))
        .finally(() => setLoading(false));
    }

    return (
        <Center padding={2}>
            <Flex direction="column">
                <Flex justify="space-between">
                  <Card.Root variant={"elevated"} minW={400} margin={2}>
                      <Card.Header>Search Hit Counter</Card.Header>
                      <Card.Body>
                          <Input placeholder="Enter your search words"
                             value={input}
                             onChange={(e) => setInput(e.target.value)}
                             onKeyDown={(e) => {
                                 if (e.key == "Enter")
                                     sendRequest();
                             }}
                             disabled={loading} />
                      </Card.Body>
                      <Card.Footer justifyContent="flex-end">
                          <Button onClick={(_) => sendRequest()} disabled={loading}>
                              Submit
                          </Button>
                      </Card.Footer>
                  </Card.Root>
                </Flex>
                <Card.Root variant={"elevated"} minW={400} margin={2}>
                    <Card.Header>Results:</Card.Header>
                    <Card.Body>{loading ? <Spinner /> :
                        <List.Root>
                                {result ?
                                    result.map((item : Result) => (
                                    <List.Item key={item.input}>
                                        {item.input + ": " + item.hits + " hits"}
                                    </List.Item>))
                                :
                                "No results received"}
                        </List.Root>
                    }
                    </Card.Body>
                </Card.Root>
            </Flex>
        </Center> 
    )
}