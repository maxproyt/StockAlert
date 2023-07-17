# StockAlert
O StockAlert é um monitor de ativos financeiros da bolsa brasileira que notifica através de e-mails caso a cotação de uma ação ultrapasse valores determinados.

## Instruções de Configuração
### Arquivos de Configuração HTML
Os arquivos **"Email_body_buy.html"** e **"Email_body_sell.html"** estão presentes na pasta "files" e não devem ser excluídos. Esses arquivos contêm a mensagem de e-mail que será enviada aos clientes e podem ser editados para personalizar o conteúdo.

### Arquivo de Configuração SMTP
O arquivo **"SMTP_Config_file.txt"** é usado para configurar o servidor SMTP e definir os destinatários dos e-mails. Ele deve seguir o formato a seguir:


```smtp.gmail.com 587 assetsnotifierchallenge@gmail.com your_password recipient1@example.com recipient2@example.com```


- A primeira palavra especifica o servidor SMTP desejado.
- A segunda palavra especifica a porta a ser utilizada na conexão SMTP.
- A terceira palavra é o e-mail de origem gerenciado pelo usuário do programa.
- A quarta palavra é a senha de apps ou outra credencial válida para o e-mail de origem.
- As palavras seguintes determinam os endereços de e-mail dos destinatários.

**Certifique-se de fornecer as informações corretas no arquivo de configuração para que o programa possa enviar e-mails corretamente.**
