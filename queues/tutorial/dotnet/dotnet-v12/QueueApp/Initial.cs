//----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace QueueAppInitial
{
    class Initial
    {
        static async Task SendNonExpiringMessageAsync(QueueClient theQueue, string message)
        {
            // <snippet_SendNonExpiringMessage>
            await theQueue.SendMessageAsync(message, default, TimeSpan.FromSeconds(-1), default);
            // </snippet_SendNonExpiringMessage>
        }

        // <snippet_InitialReceiveMessage>
        static async Task<string> ReceiveMessageAsync(QueueClient theQueue)
        {
            if (await theQueue.ExistsAsync())
            {
                QueueProperties properties = await theQueue.GetPropertiesAsync();

                if (properties.ApproximateMessagesCount > 0)
                {
                    QueueMessage[] retrievedMessage = await theQueue.ReceiveMessagesAsync(1);
                    string theMessage = retrievedMessage[0].MessageText;
                    await theQueue.DeleteMessageAsync(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
                    return theMessage;
                }

                return null;
            }

            return null;
        }
        // </snippet_InitialReceiveMessage>
    }

}