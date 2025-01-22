import { useState } from "react"
import { Button, Card, Center, Flex, Input } from "@chakra-ui/react"

function App() {
  const [input, setInput] = useState("")

  return (
    <Center padding={2}>
        <Flex justify="space-between">
          <Card.Root variant={"elevated"} minW={400}>
              <Card.Header>Search Hit Counter</Card.Header>
              <Card.Body>
                  <Input placeholder="Enter your search words"
                         value={input}
                         onChange={(e) => setInput(e.target.value)} />
              </Card.Body>
              <Card.Footer justifyContent="flex-end">
                  <Button onClick={(_) => setInput("")}>Send</Button>
              </Card.Footer>
          </Card.Root>
        </Flex>
    </Center> 
  )
}

export default App
