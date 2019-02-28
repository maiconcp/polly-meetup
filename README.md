#### Criando Aplicações Resilientes

Código fonte da aplicação realizada em 27/fev/2019 no II meetup .Net

#### Aplicação
São dois projetos:
###### ConsoleClient
Faz o papel de uma aplicação cliente. É aqui que estão configuradas as políticas de resiliência.

###### TargetServiceConfig
Faz o papel do servidor.
O cliente irá consumir a API `api/business`
Porém a aplicação também tem uma pagina web para simular problemas no servidor.

###### Exemplo
![gif](https://github.com/maiconcp/polly-meetup/blob/master/Gifs/AppDemo.gif?raw=true)

#### Slides da Apresentação

Disponível no [SlideShare](https://www.slideshare.net/MaiconPereira/criando-aplicaes-resilientes-133708969)

#### Animações

Disponíveis na pasta `Gifs`

![Retry.gif](https://github.com/maiconcp/polly-meetup/blob/master/Gifs/Retry.gif?raw=true) 

![gif](https://github.com/maiconcp/polly-meetup/blob/master/Gifs/CircuitBreaker.gif?raw=true)

![Fallback.gif](https://github.com/maiconcp/polly-meetup/blob/master/Gifs/Fallback.gif?raw=true)

![Timeout.gif](https://github.com/maiconcp/polly-meetup/blob/master/Gifs/Timeout.gif?raw=true)