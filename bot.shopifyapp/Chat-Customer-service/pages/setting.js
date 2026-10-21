
import React from 'react';
import '@shopify/polaris/styles.css';
import {
  Form, Button, FormLayout, TextField, Select,
  Frame, SettingToggle, Layout, Page,
} from '@shopify/polaris';
import axios from 'axios';
import { ConnectionBase } from 'mongoose';


const options = [
  { label: 'Open Sans', value: 'Open+Sans' },
  { label: 'Roboto', value: 'Roboto' },
  { label: 'Lato', value: 'Lato' },
];
export default class Settings extends React.Component {
  constructor() {
    super()
    this.state = {
      open: false,
      assetValue: '',
      widgetfontFamily: 'Open+Sans',
      newlocalData: '',
      name: '',
      avarat_url: '',
      url: '',
      id: '',
      user_avarat_url: '',
      head_title: '',
      first_message_text: '',
      description: '',
      chart_now_btn: '',
      register_btn: '',
      formName: '',
      formNamePlaceholder: '',
      formEmail: '',
      formEmailPlaceholder: '',
      widgetfontFamily: '',
      widgetwith: '',
      widgetRight: '',
      widgetBottom: '',
    }
  }
  handleChange = (newValue) => {
    this.setState({ widgetfontFamily: newValue });
  };

  callApi = async () => {
    const response = await fetch('https://shopifyapp.botcreator.online:5000/api/get');
    const body = await response.json();
    if (response.status !== 200) throw Error(body.message);
    return body;
  };

  onOpenModal = () => {
    this.setState({ open: true });
  };

  onCloseModal = () => {
    this.setState({ open: false });
  };

  handleSubmit = (event) => {
    this.setState({ open: true });
    const name = this.state.name
    const url = this.state.url
    const avarat_url = this.state.avarat_url
    const id = this.state.id
    const user_avarat_url = this.state.user_avarat_url
    const head_title = this.state.head_title
    const first_message_text = this.state.first_message_text
    const description = this.state.description
    const chart_now_btn = this.state.chart_now_btn
    const register_btn = this.state.register_btn
    const formName = this.state.formName
    const formNamePlaceholder = this.state.formNamePlaceholder
    const formEmail = this.state.formEmail
    const formEmailPlaceholder = this.state.formEmailPlaceholder
    const widgetfontFamily = this.state.widgetfontFamily
    const widgetwith = this.state.widgetwith
    const widgetRight = this.state.widgetRight
    const widgetBottom = this.state.widgetBottom

    const finalDAta = {
      name,
      avarat_url,
      url,
      id,
      user_avarat_url,
      head_title,
      first_message_text,
      description,
      chart_now_btn,
      register_btn,
      formName,
      formNamePlaceholder,
      formEmail,
      formEmailPlaceholder,
      widgetfontFamily,
      widgetwith,
      widgetRight,
      widgetBottom
    }

    var string =
      `<link rel="stylesheet" href="https://botcreator.azureedge.net/css/styles.css">
<script src="https://cdn.pubnub.com/sdk/javascript/pubnub.4.21.7.js"></script>
<script src="https://botcreator.azureedge.net/javascript/helpWidget.js"></script>
<script>

help_widget.widget.init({
  stylesheet: {},
  bot: {
      id: '${this.state.id}'
  }
});

</script>`
    const valueAsset = this.state.assetValue
    const assetNewValu = {
      valueAsset
    }  
    
      let bodyData = {
        method: 'POST',
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(assetNewValu),
      }
      fetch('https://shopifyapp.botcreator.online:5000/api/post', bodyData).then(response => {
        console.log(response)
      })
        .catch(error => {
          return error;
        });
    
    const newData = {
      string,
      finalDAta
    }
    console.log(newData)
    axios({
      method: 'post',
      headers: {
        'Content-Type': 'application/json',
      },
      url: 'https://shopifyapp.botcreator.online:5000/api',
      data: JSON.stringify(newData)
    }).then((response) => {
      console.log(response)


      var responseData = response.data.body.formData
      console.log(responseData)

      var mongodatalocal = JSON.stringify(responseData)
      if (response.status == 200) {
        window.localStorage.setItem('mydatamongo', mongodatalocal)
        window.location.reload();
        alert('success');
      }
    })
  };
  componentDidMount() {
    this.callApi()
      .then((res) =>
        this.setState({
          assetValue: res.data.asset.value
        }),
      )
      .catch(err => console.log(err));
    var localDAta = window.localStorage.getItem('mydatamongo')
    var localData1 = JSON.parse(localDAta)
    if (localData1 == null || localData1 == undefined || localData1 == '') {
      this.setState({
        name: '',
        avarat_url: '',
        url: '',
        id: '',
        user_avarat_url: '',
        head_title: '',
        first_message_text: '',
        description: '',
        chart_now_btn: '',
        register_btn: '',
        formName: '',
        formNamePlaceholder: '',
        formEmail: '',
        formEmailPlaceholder: '',
        widgetfontFamily: '',
        widgetwith: '',
        widgetRight: '',
        widgetBottom: '',

      })
    } else {
      this.setState({
        name: localData1.name,
        avarat_url: localData1.avarat_url,
        url: localData1.url,
        id: localData1.id,
        user_avarat_url: localData1.user_avarat_url,
        head_title: localData1.head_title,
        first_message_text: localData1.first_message_text,
        description: localData1.description,
        chart_now_btn: localData1.chart_now_btn,
        register_btn: localData1.register_btn,
        formName: localData1.formName,
        formNamePlaceholder: localData1.formNamePlaceholder,
        formEmail: localData1.formEmail,
        formEmailPlaceholder: localData1.formEmailPlaceholder,
        widgetfontFamily: localData1.widgetfontFamily,
        widgetwith: localData1.widgetwith,
        widgetRight: localData1.widgetRight,
        widgetBottom: localData1.widgetBottom
      }, function () {
      });
    }
  }

  render() {

    console.log('aset', this.state.assetValue)
    const { open, newlocalData } = this.state;
    console.log(this.state.newlocalData)
    return (
      <div>
        {/* <Form onSubmit={this.handleSubmit}> */}
        <Form>
          <Frame>
            <Page >
              <Layout>
                <Layout.Section>
                  <FormLayout>
                    <SettingToggle>
                      <FormLayout.Group>
                        <TextField type="text" label="Bot Name:"
                          value={this.state.name} id="10"
                          onChange={name => { this.setState({ name }) }} />
                        <TextField type="text" label="Bot URL:"
                          value={this.state.url} id="11"
                          onChange={url => { this.setState({ url }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="Bot Avatar URL:"
                          value={this.state.avarat_url} id="102"
                          onChange={avarat_url => { this.setState({ avarat_url }) }} />
                        <TextField type="text" label="Bot ID:"
                          id="28" value={this.state.id}
                          onChange={id => { this.setState({ id }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="User Avatar URL:"
                          // placeholder={`${this.state.selected}`}
                          id="12" value={this.state.user_avarat_url}
                          onChange={user_avarat_url => { this.setState({ user_avarat_url }) }} />
                        <TextField type="text" label="Head Title:"
                          id="23" value={this.state.head_title}
                          onChange={head_title => { this.setState({ head_title }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="First Message Text:"
                          id="13" value={this.state.first_message_text}
                          onChange={first_message_text => { this.setState({ first_message_text }) }} />
                        <TextField type="text" label="Description"
                          id="21" value={this.state.description}
                          onChange={description => { this.setState({ description }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="Buttons-Chart Now:"
                          id="14" value={this.state.chart_now_btn}
                          onChange={chart_now_btn => { this.setState({ chart_now_btn }) }} />
                        <TextField type="text" label="Buttons-Register"
                          id="20" value={this.state.register_btn}
                          onChange={register_btn => { this.setState({ register_btn }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="Form-Name->Title"
                          id="15" value={this.state.formName}
                          onChange={formName => { this.setState({ formName }) }} />
                        <TextField type="text" label="Form-Name-> Placeholder"
                          id="18" value={this.state.formNamePlaceholder}
                          onChange={formNamePlaceholder => { this.setState({ formNamePlaceholder }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="Form-Email->Title"
                          id="16" value={this.state.formEmail}
                          onChange={formEmail => { this.setState({ formEmail }) }} />
                        <TextField type="text" label="Form-Email->Placeholder"
                          id="17" value={this.state.formEmailPlaceholder}
                          onChange={formEmailPlaceholder => { this.setState({ formEmailPlaceholder }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <Select
                          label="Style-Widget->Font Family"
                          options={options}
                          onChange={this.handleChange}
                          // value={this.state.selected}
                          value={this.state.widgetfontFamily}
                        />
                        <TextField type="text" label="Styles-Widget->Width:"
                          id="31" value={this.state.widgetwith}
                          onChange={widgetwith => { this.setState({ widgetwith }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <TextField type="text" label="Styles-Widget->Right:"
                          id="32" value={this.state.widgetRight}
                          onChange={widgetRight => { this.setState({ widgetRight }) }} />
                        <TextField type="text" label="Styles-Widget->Bottom:"
                          id="34" value={this.state.widgetBottom}
                          onChange={widgetBottom => { this.setState({ widgetBottom }) }} />
                      </FormLayout.Group>
                      <FormLayout.Group>
                        <Button onClick={this.handleSubmit} >Submit</Button>
                      </FormLayout.Group>
                    </SettingToggle>
                  </FormLayout>
                </Layout.Section>
              </Layout>
            </Page>
          </Frame>
        </Form>
      </div>
    )
  }
}