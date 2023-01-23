using ConsumerMensagemIA.DomainReceiver.Services;
using ConsumerMensagemIA.DomainReceiver.Services.Interfaces;
using ConsumerMensagemIA.Integration.Facades;
using ConsumerMensagemIA.Integration.Facades.Interfaces;

IEmailFacade emailFacade = new EmailFacade();
IMensagemService mensagemService = new MensagemService(emailFacade);

mensagemService.ConsumeMessage();